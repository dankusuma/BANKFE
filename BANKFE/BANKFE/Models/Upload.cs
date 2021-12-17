using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BANKFE.Models
{
    public class Upload
    {
        string photoName { get; set; }
        string videoName { get; set; }

        public IFormFile uploadedPhoto { get; set; }
        public IFormFile uploadedVideo { get; set; }

        public string stringPhoto { get; set; }
        public string stringVideo { get; set; }

        public Upload(string photoName, string videoName)
        {
            this.photoName = photoName;
            this.videoName = videoName;
        }

        public void convertToString()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                uploadedPhoto.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();
                stringPhoto = Convert.ToBase64String(fileBytes);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                uploadedVideo.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();
                stringVideo = Convert.ToBase64String(fileBytes);
            }
        }

        public void convertToFile()
        {
            byte[] photoBytes = Convert.FromBase64String(stringPhoto);
            MemoryStream msPhoto = new MemoryStream(photoBytes);
            uploadedPhoto = new FormFile(msPhoto, 0, photoBytes.Length, photoName, photoName);
            msPhoto.Close();

            byte[] videoBytes = Convert.FromBase64String(stringVideo);
            MemoryStream msVideo = new MemoryStream(videoBytes);
            uploadedVideo = new FormFile(msVideo, 0, videoBytes.Length, videoName, videoName);
            msVideo.Close();

        }
    }
}
