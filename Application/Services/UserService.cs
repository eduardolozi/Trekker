using Application.Interfaces;
using Domain.DTOs;
using Domain.Filters;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Integrations;

namespace Application.Services;

public class UserService(IExternalAuthService externalAuthService, IUserRepository userRepository, S3Service s3Service) : IUserService
{
    public async Task CreateUser(User user)
    {
        try
        {
            user.KeycloakId = await externalAuthService.RegisterUser(user);
            await userRepository.InsertUser(user);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task PutPhoto(int id, FileDTO file)
    {
        file.FileName = file.FileName.Replace(" ", string.Empty);
        var fileKey = await s3Service.PutObject(file);
        await userRepository.PutPhoto(id, fileKey);
    }

    public async Task<User?> GetUser(int id, UserFilter filter)
    {
        var user = await userRepository.FindUser(id, filter);

        if (user != null && !string.IsNullOrEmpty(user.PhotoPath))
        {
            user.PhotoPath = await s3Service.GetPresignedUrl(user.PhotoPath);
        }

        return user;
    }
}