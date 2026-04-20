using System;
using Amazon.S3;
using Amazon.S3.Model;


namespace movie_reservation_system.Infrastructure;

public interface IS3Storage
{
    Task<string> UploadImage (IFormFile file, string fileName);
}



public class S3Storage : IS3Storage
{
    private readonly AmazonS3Client _s3client;
    private readonly string _bucket;

    public S3Storage (IConfiguration config)
    {
        var s3config = config.GetSection("S3Storage");

        var amazonS3Config = new AmazonS3Config {
            ServiceURL= s3config["Url"],
            ForcePathStyle= true
        };

        _s3client = new AmazonS3Client (
            s3config["AccessKey"],
            s3config["SecretKey"],
            s3config["Region"],
            amazonS3Config
        );

        _bucket = s3config["BucketName"]!;
    }

    public async Task<string> UploadImage (IFormFile file, string fileName)
    {
        var uploadRequest = new PutObjectRequest
        {
            BucketName= _bucket,
            Key= $"movie/{fileName}",
            InputStream= file.OpenReadStream(),
            ContentType= file.ContentType,
            DisablePayloadSigning= true
        };

        await _s3client.PutObjectAsync(uploadRequest);

        return $"movie/{fileName}";
    }
}
