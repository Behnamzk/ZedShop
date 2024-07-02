using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace ZedShop.Core.Convertors
{
    public class ImageConvertor
    {
        public void ResizeImage(string inputImagePath, string outputImagePath, int newWidth)
        {

            const long quality = 50L;

            Bitmap sourceBitmap = new Bitmap(inputImagePath);



            double dblWidthOrigial = sourceBitmap.Width;

            double dblHeigthOrigial = sourceBitmap.Height;

            double relationHeigthWidth = dblHeigthOrigial / dblWidthOrigial;

            int newHeight = (int)(newWidth * relationHeigthWidth);



            //< create Empty Drawarea >

            var newDrawArea = new Bitmap(newWidth, newHeight);

            //</ create Empty Drawarea >



            using (var graphicOfDrawArea = Graphics.FromImage(newDrawArea))

            {

                //< setup >

                graphicOfDrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphicOfDrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphicOfDrawArea.CompositingMode = CompositingMode.SourceCopy;

                //</ setup >



                //< draw into placeholder >

                //*imports the image into the drawarea

                graphicOfDrawArea.DrawImage(sourceBitmap, 0, 0, newWidth, newHeight);

                //</ draw into placeholder >



                //--< Output as .Jpg >--

                using (var output = System.IO.File.Open(outputImagePath, FileMode.Create))

                {

                    //< setup jpg >

                    var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    //</ setup jpg >



                    //< save Bitmap as Jpg >

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    newDrawArea.Save(output, codec, encoderParameters);

                    //resized_Bitmap.Dispose ();

                    output.Close();

                    //</ save Bitmap as Jpg >

                }

                //--</ Output as .Jpg >--

                graphicOfDrawArea.Dispose();

            }

            sourceBitmap.Dispose();

            //---------------</ Image_resize() >---------------

        }



        public void ResizeImage(IFormFile inputFile, string outputImagePath, int newWidth)
        {

            const long quality = 50L;

            Bitmap sourceBitmap = new Bitmap(inputFile.OpenReadStream());



            double dblWidthOrigial = sourceBitmap.Width;

            double dblHeigthOrigial = sourceBitmap.Height;

            double relationHeigthWidth = dblHeigthOrigial / dblWidthOrigial;

            int newHeight = (int)(newWidth * relationHeigthWidth);



            //< create Empty Drawarea >

            var newDrawArea = new Bitmap(newWidth, newHeight);

            //</ create Empty Drawarea >



            using (var graphicOfDrawArea = Graphics.FromImage(newDrawArea))

            {

                //< setup >

                graphicOfDrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphicOfDrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphicOfDrawArea.CompositingMode = CompositingMode.SourceCopy;

                //</ setup >



                //< draw into placeholder >

                //*imports the image into the drawarea

                graphicOfDrawArea.DrawImage(sourceBitmap, 0, 0, newWidth, newHeight);

                //</ draw into placeholder >



                //--< Output as .Jpg >--

                using (var output = System.IO.File.Open(outputImagePath, FileMode.Create))

                {

                    //< setup jpg >

                    var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    //</ setup jpg >



                    //< save Bitmap as Jpg >

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    newDrawArea.Save(output, codec, encoderParameters);

                    //resized_Bitmap.Dispose ();

                    output.Close();

                    //</ save Bitmap as Jpg >

                }

                //--</ Output as .Jpg >--

                graphicOfDrawArea.Dispose();

            }

            sourceBitmap.Dispose();

            //---------------</ Image_resize() >---------------

        }
    }
}
