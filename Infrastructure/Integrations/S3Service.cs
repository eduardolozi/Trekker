using Amazon.S3;
using Amazon.S3.Model;
using Domain.DTOs;
using Domain.Utils;

namespace Infrastructure.Integrations;

public class S3Service(IAmazonS3 amazonS3Client)
{
    private readonly string _bucketName = TrekkerEnvironment.S3BucketName;

    public async Task<string> PutObject(FileDTO file)
    {
        var folder = file.ContentType switch
        {
            "image/png" or "image/jpeg" => "images",
            "video/mp4" or "video/webm" => "videos",
            "audio/mpeg" => "audios",
            "application/pdf" => "pdfs",
            _ => "images"
        };
        
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = $"{folder}/{file.FileName}",
            ContentType = file.ContentType,
            InputStream = file.Stream
        };

        await amazonS3Client.PutObjectAsync(request);
        return $"https://trekkerbucket.s3.us-east-2.amazonaws.com/{folder}/{file.FileName}";
    }
}