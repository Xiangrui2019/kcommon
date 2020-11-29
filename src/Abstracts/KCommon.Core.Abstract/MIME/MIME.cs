﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.Abstract.MIME
{
    public class MIME
    {
        private static bool HasKey(string extension)
        {
            var lower = extension.ToLower().TrimStart('.');
            return MimeTypesDictionary.ContainsKey(lower);
        }

        public static bool CanHandle(string fileName)
        {
            var extension = Path.GetExtension(fileName).Trim('.');
            var contentType = GetContentType(extension);
            return contentType != "application/octet-stream";
        }

        public static bool IsVideo(string fileName)
        {
            var extension = Path.GetExtension(fileName).Trim('.');
            var contentType = GetContentType(extension);
            return contentType.StartsWith("video");
        }

        public static string GetContentType(string extenstion)
        {
            //Not to download the file, and we can process the file, let us process it.
            if (HasKey(extenstion))
            {
                string lower = extenstion.ToLower().TrimStart('.');
                return MimeTypesDictionary[lower];
            }
            return "application/octet-stream";
        }

        private static readonly Dictionary<string, string> MimeTypesDictionary = new Dictionary<string, string>
        {
            {"wav", "audio/wav"},
            {"mkv", "video/x-matroska"},
            {"avi", "video/x-msvideo"},
            {"bmp", "image/bmp"},
            {"css", "text/css"},
            {"gif", "image/gif"},
            {"htm", "text/html"},
            {"html", "text/html"},
            {"ico", "image/x-icon"},
            {"jpeg", "image/jpeg"},
            {"jpg", "image/jpeg"},
            {"json", "application/json"},
            {"js", "application/x-javascript"},
            {"map","text/plain"},
            {"m4a", "audio/mp4a-latm"},
            {"mid", "audio/midi"},
            {"mov", "video/quicktime"},
            {"mp3", "audio/mpeg"},
            {"webm","video/webm"},
            {"mp4", "video/mp4"},
            {"mpeg", "video/mpeg"},
            {"mpg", "video/mpeg"},
            {"ogg", "video/ogg"},
            {"opus", "audio/ogg"},
            {"pdf", "application/pdf"},
            {"png", "image/png"},
            {"swf", "application/x-shockwave-flash"},
            {"svg", "image/svg+xml"},
            {"tif", "image/tiff"},
            {"tiff", "image/tiff"},
            {"txt", "text/plain"},
            {"xhtml", "application/xhtml+xml"},
            {"m4u", "video/vnd.mpegurl"},
            {"woff","application/x-font-woff"},
            {"woff2","application/x-font-woff2"},
            {"ttf","application/x-font-truetype"},
            {"otf","application/x-font-opentype"},
            {"eot","application/application/vnd.ms-fontobject"},
            {"ts","application/x-typescript"},
            {"xml", "text/xml"}
        };
    }
}
