using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.AspNetMvc.Service
{
    public static class ImageHelper
    {
        static readonly long maxLength = 10485760;//10*1024*1024
        public static SkiaSharp.SKEncodedImageFormat GetImageFormatBySuffix(string suffix)
        {
            var format = SkiaSharp.SKEncodedImageFormat.Jpeg;
            if (string.IsNullOrEmpty(suffix))
            {
                return format;
            }
            if (suffix[0] == '.')
            {
                suffix = suffix.Substring(1);
            }
            if (string.IsNullOrEmpty(suffix))
            {
                return format;
            }
            suffix = suffix.ToUpper();
            switch (suffix)
            {
                case "PNG":
                    format = SkiaSharp.SKEncodedImageFormat.Png;
                    break;
                case "GIF":
                    format = SkiaSharp.SKEncodedImageFormat.Gif;
                    break;
                case "BMP":
                    format = SkiaSharp.SKEncodedImageFormat.Bmp;
                    break;
                case "ICON":
                    format = SkiaSharp.SKEncodedImageFormat.Ico;
                    break;
                case "ICO":
                    format = SkiaSharp.SKEncodedImageFormat.Ico;
                    break;
                case "DNG":
                    format = SkiaSharp.SKEncodedImageFormat.Dng;
                    break;
                case "WBMP":
                    format = SkiaSharp.SKEncodedImageFormat.Wbmp;
                    break;
                case "WEBP":
                    format = SkiaSharp.SKEncodedImageFormat.Webp;
                    break;
                case "PKM":
                    format = SkiaSharp.SKEncodedImageFormat.Pkm;
                    break;
                case "KTX":
                    format = SkiaSharp.SKEncodedImageFormat.Ktx;
                    break;
                case "ASTC":
                    format = SkiaSharp.SKEncodedImageFormat.Astc;
                    break;
            }
            return format;
        }
        public static SkiaSharp.SKEncodedImageFormat GetImageFormatByPath(string path)
        {
            var suffix = "";
            if (System.IO.Path.HasExtension(path))
            {
                suffix = System.IO.Path.GetExtension(path);
            }
            return GetImageFormatBySuffix(suffix);
        }
        public static Tuple<int, int, long, SkiaSharp.SKEncodedImageFormat> GetImageInfo(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("路径不能为空");
            }
            if (!System.IO.File.Exists(path))
            {
                throw new Exception("文件不存在");
            }
            var fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read); //fileInfo.OpenRead();
            var fileLength = fileStream.Length;
            if (fileLength > maxLength)
            {
                fileStream.Dispose();
                throw new Exception("文件过大");
            }
            var sKManagedStream = new SkiaSharp.SKManagedStream(fileStream, true);
            var sKBitmap = SkiaSharp.SKBitmap.Decode(sKManagedStream);
            sKManagedStream.Dispose();

            if (sKBitmap.IsEmpty)
            {
                sKBitmap.Dispose();
                throw new Exception("文件无效");
            }
            int w = sKBitmap.Width;
            int h = sKBitmap.Height;
            return new Tuple<int, int, long, SkiaSharp.SKEncodedImageFormat>(w, h, fileLength, GetImageFormatByPath(path));
        }
        public static void ImageMaxCutByCenter(string path, string savePath, int saveWidth, int saveHeight, int quality)
        {
            var bytes = ImageMaxCutByCenter(path, saveWidth, saveHeight, quality);
            if (bytes == null || bytes.Length < 1)
            {
                return;
            }
            string saveDirPath = System.IO.Path.GetDirectoryName(savePath);
            if (!System.IO.Directory.Exists(saveDirPath))
            {
                System.IO.Directory.CreateDirectory(saveDirPath);
            }
            System.IO.FileStream fs = new System.IO.FileStream(savePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }
        public static byte[] ImageMaxCutByCenter(string path, int saveWidth, int saveHeight, int quality)
        {
            byte[] bytes = null;
            if (!System.IO.File.Exists(path))
            {
                return bytes;
            }
            var fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read); //fileInfo.OpenRead();
            if (fileStream.Length > maxLength)
            {
                fileStream.Dispose();
                return bytes;
            }
            var sKManagedStream = new SkiaSharp.SKManagedStream(fileStream, true);
            var sKBitmap = SkiaSharp.SKBitmap.Decode(sKManagedStream);
            sKManagedStream.Dispose();

            if (sKBitmap.IsEmpty)
            {
                return bytes;
            }

            if (saveWidth < 1) { saveWidth = 1; }
            if (saveHeight < 1) { saveHeight = 1; }
            if (quality < 1) { quality = 1; }
            if (quality > 100) { quality = 100; }

            int oW = sKBitmap.Width;
            int oH = sKBitmap.Height;
            int cutW = saveWidth;
            int cutH = saveHeight;
            double ratio = 1;
            if (cutW > oW)
            {
                ratio = (double)oW / (double)cutW;
                cutH = Convert.ToInt32((double)cutH * ratio);
                cutW = oW;
                if (cutH > oH)
                {
                    ratio = (double)oH / (double)cutH;
                    cutW = Convert.ToInt32((double)cutW * ratio);
                    cutH = oH;
                }
            }
            else if (cutW < oW)
            {
                ratio = (double)oW / (double)cutW;
                cutH = Convert.ToInt32(Convert.ToDouble(cutH) * ratio);
                cutW = oW;
                if (cutH > oH)
                {
                    ratio = (double)oH / (double)cutH;
                    cutW = Convert.ToInt32((double)cutW * ratio);
                    cutH = oH;
                }
            }
            else
            {
                if (cutH > oH)
                {
                    ratio = (double)oH / (double)cutH;
                    cutW = Convert.ToInt32((double)cutW * ratio);
                    cutH = oH;
                }
            }
            int startX = oW > cutW ? (oW / 2 - cutW / 2) : (cutW / 2 - oW / 2);
            int startY = oH > cutH ? (oH / 2 - cutH / 2) : (cutH / 2 - oH / 2);

            var sKBitmap2 = new SkiaSharp.SKBitmap(saveWidth, saveHeight);
            var sKCanvas = new SkiaSharp.SKCanvas(sKBitmap2);
            var sKPaint = new SkiaSharp.SKPaint
            {
                FilterQuality = SkiaSharp.SKFilterQuality.Medium,
                IsAntialias = true
            };
            sKCanvas.DrawBitmap(
                sKBitmap,
                new SkiaSharp.SKRect
                {
                    Location = new SkiaSharp.SKPoint { X = startX, Y = startY },
                    Size = new SkiaSharp.SKSize { Height = cutH, Width = cutW }
                },
                new SkiaSharp.SKRect
                {
                    Location = new SkiaSharp.SKPoint { X = 0, Y = 0 },
                    Size = new SkiaSharp.SKSize { Height = saveHeight, Width = saveWidth }
                }, sKPaint);
            sKCanvas.Dispose();
            var sKImage2 = SkiaSharp.SKImage.FromBitmap(sKBitmap2);
            sKBitmap2.Dispose();
            var data = sKImage2.Encode(GetImageFormatByPath(path), quality);
            sKImage2.Dispose();
            bytes = data.ToArray();
            data.Dispose();

            return bytes;
        }
        public static void ImageScalingToRange(string path, string savePath, int maxWidth, int maxHeight, int quality)
        {
            var bytes = ImageScalingToRange(path, maxWidth, maxHeight, quality);
            if (bytes == null || bytes.Length < 1)
            {
                return;
            }
            string saveDirPath = System.IO.Path.GetDirectoryName(savePath);
            if (!System.IO.Directory.Exists(saveDirPath))
            {
                System.IO.Directory.CreateDirectory(saveDirPath);
            }
            System.IO.FileStream fs = new System.IO.FileStream(savePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }
        public static byte[] ImageScalingToRange(string path, int maxWidth, int maxHeight, int quality)
        {
            byte[] bytes = null;
            if (!System.IO.File.Exists(path))
            {
                return bytes;
            }
            var fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read); //fileInfo.OpenRead();
            if (fileStream.Length > maxLength)
            {
                fileStream.Dispose();
                return bytes;
            }
            var sKManagedStream = new SkiaSharp.SKManagedStream(fileStream, true);
            var sKBitmap = SkiaSharp.SKBitmap.Decode(sKManagedStream);
            sKManagedStream.Dispose();

            if (sKBitmap.IsEmpty)
            {
                return bytes;
            }

            if (maxWidth < 1) { maxWidth = 1; }
            if (maxHeight < 1) { maxHeight = 1; }
            if (quality < 1) { quality = 1; }
            if (quality > 100) { quality = 100; }

            int oW = sKBitmap.Width;
            int oH = sKBitmap.Height;
            int nW = oW;
            int nH = oH;

            if (nW < maxWidth && nH < maxHeight)//放大
            {
                if (nW < maxWidth)
                {
                    var r = (double)maxWidth / (double)nW;
                    nW = maxWidth;
                    nH = (int)Math.Floor((double)nH * r);
                }
                if (nH < maxHeight)
                {
                    var r = (double)maxHeight / (double)nH;
                    nH = maxHeight;
                    nW = (int)Math.Floor((double)nW * r);
                }
            }
            //限制超出(缩小)
            if (nW > maxWidth)
            {
                var r = (double)maxWidth / (double)nW;
                nW = maxWidth;
                nH = (int)Math.Floor((double)nH * r);
            }
            if (nH > maxHeight)
            {
                var r = (double)maxHeight / (double)nH;
                nH = maxHeight;
                nW = (int)Math.Floor((double)nW * r);
            }


            var sKBitmap2 = new SkiaSharp.SKBitmap(nW, nH);
            var sKCanvas = new SkiaSharp.SKCanvas(sKBitmap2);
            var sKPaint = new SkiaSharp.SKPaint
            {
                FilterQuality = SkiaSharp.SKFilterQuality.Medium,
                IsAntialias = true
            };
            sKCanvas.DrawBitmap(
                sKBitmap,
                new SkiaSharp.SKRect
                {
                    Location = new SkiaSharp.SKPoint { X = 0, Y = 0 },
                    Size = new SkiaSharp.SKSize { Height = oH, Width = oW }
                },
                new SkiaSharp.SKRect
                {
                    Location = new SkiaSharp.SKPoint { X = 0, Y = 0 },
                    Size = new SkiaSharp.SKSize { Height = nH, Width = nW }
                }, sKPaint);
            sKCanvas.Dispose();
            var sKImage2 = SkiaSharp.SKImage.FromBitmap(sKBitmap2);
            sKBitmap2.Dispose();
            var data = sKImage2.Encode(GetImageFormatByPath(path), quality);
            sKImage2.Dispose();
            bytes = data.ToArray();
            data.Dispose();

            return bytes;
        }
        public static void ImageScalingByOversized(string path, string savePath, int maxWidth, int maxHeight, int quality)
        {
            var bytes = ImageScalingByOversized(path, maxWidth, maxHeight, quality);
            if (bytes == null || bytes.Length < 1)
            {
                return;
            }
            string saveDirPath = System.IO.Path.GetDirectoryName(savePath);
            if (!System.IO.Directory.Exists(saveDirPath))
            {
                System.IO.Directory.CreateDirectory(saveDirPath);
            }
            System.IO.FileStream fs = new System.IO.FileStream(savePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }
        public static byte[] ImageScalingByOversized(string path, int maxWidth, int maxHeight, int quality)
        {
            byte[] bytes = null;
            if (!System.IO.File.Exists(path))
            {
                return bytes;
            }
            var fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            if (fileStream.Length > maxLength)
            {
                fileStream.Dispose();
                return bytes;
            }
            var sKManagedStream = new SkiaSharp.SKManagedStream(fileStream, true);
            var sKBitmap = SkiaSharp.SKBitmap.Decode(sKManagedStream);
            sKManagedStream.Dispose();

            if (sKBitmap.IsEmpty)
            {
                return bytes;
            }

            if (maxWidth < 1) { maxWidth = 1; }
            if (maxHeight < 1) { maxHeight = 1; }
            if (quality < 1) { quality = 1; }
            if (quality > 100) { quality = 100; }

            int oW = sKBitmap.Width;
            int oH = sKBitmap.Height;
            int nW = oW;
            int nH = oH;

            if (oW > maxWidth || oH > maxHeight)
            {
                nW = maxWidth;
                nH = maxHeight;
                double ratio = 1;

                if (nW > 0 && nH > 0)
                {
                    ratio = (double)nW / oW;
                    nH = Convert.ToInt32(oH * ratio);
                    if (maxHeight < nH)
                    {
                        ratio = (double)maxHeight / nH;
                        nW = Convert.ToInt32(nW * ratio);
                        nH = maxHeight;
                    }
                }
                if (nW < 1 && nH < 1)
                {
                    nW = oW;
                    nH = oH;
                }
                if (nW < 1)
                {
                    ratio = (double)nH / oH;
                    nW = Convert.ToInt32(oW * ratio);
                }
                if (nH < 1)
                {
                    ratio = (double)nW / oW;
                    nH = Convert.ToInt32(oH * ratio);
                }
                var sKBitmap2 = new SkiaSharp.SKBitmap(nW, nH);
                var sKCanvas = new SkiaSharp.SKCanvas(sKBitmap2);
                var sKPaint = new SkiaSharp.SKPaint
                {
                    FilterQuality = SkiaSharp.SKFilterQuality.Medium,
                    IsAntialias = true
                };
                sKCanvas.DrawBitmap(
                    sKBitmap,
                    new SkiaSharp.SKRect
                    {
                        Location = new SkiaSharp.SKPoint { X = 0, Y = 0 },
                        Size = new SkiaSharp.SKSize { Height = oH, Width = oW }
                    },
                    new SkiaSharp.SKRect
                    {
                        Location = new SkiaSharp.SKPoint { X = 0, Y = 0 },
                        Size = new SkiaSharp.SKSize { Height = nH, Width = nW }
                    }, sKPaint);
                sKCanvas.Dispose();
                sKBitmap.Dispose();
                sKBitmap = sKBitmap2;
            }

            var sKImage = SkiaSharp.SKImage.FromBitmap(sKBitmap);
            sKBitmap.Dispose();
            var data = sKImage.Encode(GetImageFormatByPath(path), quality);
            sKImage.Dispose();
            bytes = data.ToArray();
            data.Dispose();

            return bytes;
        }

        /// <summary>
        /// 生成二维码(320*320)
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="logoPath">Logo图片路径(缩放到真实二维码区域尺寸的1/6)</param>
        /// <param name="keepWhiteBorderPixelVal">白边处理(负值表示不做处理，最大值不超过真实二维码区域的1/10)</param>
        public static void QRCoder(string text, string savePath, string logoPath = "", int keepWhiteBorderPixelVal = -1)
        {
            var format = GetImageFormatByPath(savePath);
            byte[] bytesLogo = null;
            if (!string.IsNullOrEmpty(logoPath) && System.IO.File.Exists(logoPath))
            {
                var fsLogo = new System.IO.FileStream(logoPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                fsLogo.CopyTo(ms);
                fsLogo.Dispose();
                bytesLogo = ms.ToArray();
                ms.Dispose();
            }

            var bytes = QRCoder(text, format, bytesLogo, keepWhiteBorderPixelVal);

            if (bytes == null || bytes.Length < 1)
            {
                return;
            }

            var saveDirPath = System.IO.Path.GetDirectoryName(savePath);
            if (!System.IO.Directory.Exists(saveDirPath))
            {
                System.IO.Directory.CreateDirectory(saveDirPath);
            }
            var fs = new System.IO.FileStream(savePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }
        /// <summary>
        /// 生成二维码(320*320)
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="format">保存格式</param>
        /// <param name="logoImgae">Logo图片(缩放到真实二维码区域尺寸的1/6)</param>
        /// <param name="keepWhiteBorderPixelVal">白边处理(负值表示不做处理，最大值不超过真实二维码区域的1/10)</param>
        /// <returns></returns>
        public static byte[] QRCoder(string text, SkiaSharp.SKEncodedImageFormat format, byte[] logoImgae = null, int keepWhiteBorderPixelVal = -1)
        {
            byte[] reval = null;
            int width = 320;
            int height = 320;
            var qRCodeWriter = new ZXing.QrCode.QRCodeWriter();
            var hints = new Dictionary<ZXing.EncodeHintType, object>();
            hints.Add(ZXing.EncodeHintType.CHARACTER_SET, "utf-8");
            hints.Add(ZXing.EncodeHintType.QR_VERSION, 8);
            hints.Add(ZXing.EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.Q);
            var bitMatrix = qRCodeWriter.encode(text, ZXing.BarcodeFormat.QR_CODE, width, height, hints);
            var w = bitMatrix.Width;
            var h = bitMatrix.Height;
            var sKBitmap = new SkiaSharp.SKBitmap(w, h);

            int blackStartPointX = 0;
            int blackStartPointY = 0;
            int blackEndPointX = w;
            int blackEndPointY = h;

            #region --绘制二维码(同时获取真实的二维码区域起绘点和结束点的坐标)--
            var sKCanvas = new SkiaSharp.SKCanvas(sKBitmap);
            var sKColorBlack = SkiaSharp.SKColor.Parse("000000");
            var sKColorWihte = SkiaSharp.SKColor.Parse("ffffff");
            sKCanvas.Clear(sKColorWihte);
            bool blackStartPointIsNotWriteDown = true;
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var flag = bitMatrix[x, y];
                    if (flag)
                    {
                        if (blackStartPointIsNotWriteDown)
                        {
                            blackStartPointX = x;
                            blackStartPointY = y;
                            blackStartPointIsNotWriteDown = false;
                        }
                        blackEndPointX = x;
                        blackEndPointY = y;
                        sKCanvas.DrawPoint(x, y, sKColorBlack);
                    }
                    else
                    {
                        //sKCanvas.DrawPoint(x, y, sKColorWihte);//不用绘制(背景是白色的)
                    }
                }
            }
            sKCanvas.Dispose();
            #endregion

            int qrcodeRealWidth = blackEndPointX - blackStartPointX;
            int qrcodeRealHeight = blackEndPointY - blackStartPointY;

            #region -- 处理白边 --
            if (keepWhiteBorderPixelVal > -1)//指定了边框宽度
            {
                var borderMaxWidth = (int)Math.Floor((double)qrcodeRealWidth / 10);
                if (keepWhiteBorderPixelVal > borderMaxWidth)
                {
                    keepWhiteBorderPixelVal = borderMaxWidth;
                }
                var nQrcodeRealWidth = width - keepWhiteBorderPixelVal - keepWhiteBorderPixelVal;
                var nQrcodeRealHeight = height - keepWhiteBorderPixelVal - keepWhiteBorderPixelVal;

                var sKBitmap2 = new SkiaSharp.SKBitmap(width, height);
                var sKCanvas2 = new SkiaSharp.SKCanvas(sKBitmap2);
                sKCanvas2.Clear(sKColorWihte);
                //二维码绘制到临时画布上时无需抗锯齿等处理(避免文件增大)
                sKCanvas2.DrawBitmap(
                    sKBitmap,
                    new SkiaSharp.SKRect
                    {
                        Location = new SkiaSharp.SKPoint { X = blackStartPointX, Y = blackStartPointY },
                        Size = new SkiaSharp.SKSize { Height = qrcodeRealHeight, Width = qrcodeRealWidth }
                    },
                    new SkiaSharp.SKRect
                    {
                        Location = new SkiaSharp.SKPoint { X = keepWhiteBorderPixelVal, Y = keepWhiteBorderPixelVal },
                        Size = new SkiaSharp.SKSize { Width = nQrcodeRealWidth, Height = nQrcodeRealHeight }
                    });

                blackStartPointX = keepWhiteBorderPixelVal;
                blackStartPointY = keepWhiteBorderPixelVal;
                qrcodeRealWidth = nQrcodeRealWidth;
                qrcodeRealHeight = nQrcodeRealHeight;

                sKCanvas2.Dispose();
                sKBitmap.Dispose();
                sKBitmap = sKBitmap2;
            }
            #endregion

            #region -- 绘制LOGO --
            if (logoImgae != null && logoImgae.Length > 0)
            {
                SkiaSharp.SKBitmap sKBitmapLogo = SkiaSharp.SKBitmap.Decode(logoImgae);
                if (!sKBitmapLogo.IsEmpty)
                {
                    var sKPaint2 = new SkiaSharp.SKPaint
                    {
                        FilterQuality = SkiaSharp.SKFilterQuality.None,
                        IsAntialias = true
                    };
                    var logoTargetMaxWidth = (int)Math.Floor((double)qrcodeRealWidth / 6);
                    var logoTargetMaxHeight = (int)Math.Floor((double)qrcodeRealHeight / 6);
                    var qrcodeCenterX = (int)Math.Floor((double)qrcodeRealWidth / 2);
                    var qrcodeCenterY = (int)Math.Floor((double)qrcodeRealHeight / 2);
                    var logoResultWidth = sKBitmapLogo.Width;
                    var logoResultHeight = sKBitmapLogo.Height;
                    if (logoResultWidth > logoTargetMaxWidth)
                    {
                        var r = (double)logoTargetMaxWidth / logoResultWidth;
                        logoResultWidth = logoTargetMaxWidth;
                        logoResultHeight = (int)Math.Floor(logoResultHeight * r);
                    }
                    if (logoResultHeight > logoTargetMaxHeight)
                    {
                        var r = (double)logoTargetMaxHeight / logoResultHeight;
                        logoResultHeight = logoTargetMaxHeight;
                        logoResultWidth = (int)Math.Floor(logoResultWidth * r);
                    }
                    var pointX = qrcodeCenterX - (int)Math.Floor((double)logoResultWidth / 2) + blackStartPointX;
                    var pointY = qrcodeCenterY - (int)Math.Floor((double)logoResultHeight / 2) + blackStartPointY;

                    var sKCanvas3 = new SkiaSharp.SKCanvas(sKBitmap);
                    var sKPaint = new SkiaSharp.SKPaint
                    {
                        FilterQuality = SkiaSharp.SKFilterQuality.Medium,
                        IsAntialias = true
                    };
                    sKCanvas3.DrawBitmap(
                        sKBitmapLogo,
                        new SkiaSharp.SKRect
                        {
                            Location = new SkiaSharp.SKPoint { X = 0, Y = 0 },
                            Size = new SkiaSharp.SKSize { Height = sKBitmapLogo.Height, Width = sKBitmapLogo.Width }
                        },
                        new SkiaSharp.SKRect
                        {
                            Location = new SkiaSharp.SKPoint { X = pointX, Y = pointY },
                            Size = new SkiaSharp.SKSize { Height = logoResultHeight, Width = logoResultWidth }
                        }, sKPaint);
                    sKCanvas3.Dispose();
                    sKPaint.Dispose();
                    sKBitmapLogo.Dispose();
                }
                else
                {
                    sKBitmapLogo.Dispose();
                }
            }
            #endregion

            SkiaSharp.SKImage sKImage = SkiaSharp.SKImage.FromBitmap(sKBitmap);
            sKBitmap.Dispose();
            var data = sKImage.Encode(format, 75);
            sKImage.Dispose();
            reval = data.ToArray();
            data.Dispose();

            return reval;
        }
        public static string QRDecoder(string qrCodeFilePath)
        {
            if (!System.IO.File.Exists(qrCodeFilePath))
            {
                throw new Exception("文件不存在");
            }

            System.IO.FileStream fileStream = new System.IO.FileStream(qrCodeFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            if (fileStream.Length > maxLength)
            {
                fileStream.Dispose();
                throw new Exception("图片文件太大");
            }
            return QRDecoder(fileStream);
        }
        public static string QRDecoder(byte[] qrCodeBytes)
        {
            if (qrCodeBytes == null || qrCodeBytes.Length < 1)
            {
                throw new Exception("参数qrCodeBytes不存在");
            }
            if (qrCodeBytes.Length > maxLength)
            {
                throw new Exception("图片文件太大");
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream(qrCodeBytes);
            return QRDecoder(ms);
        }
        public static string QRDecoder(System.IO.Stream qrCodeFileStream)
        {
            var sKManagedStream = new SkiaSharp.SKManagedStream(qrCodeFileStream, true);
            var sKBitmap = SkiaSharp.SKBitmap.Decode(sKManagedStream);
            sKManagedStream.Dispose();
            if (sKBitmap.IsEmpty)
            {
                sKBitmap.Dispose();
                throw new Exception("未识别的图片文件");
            }

            var w = sKBitmap.Width;
            var h = sKBitmap.Height;
            int ps = w * h;
            byte[] bytes = new byte[ps * 3];
            int byteIndex = 0;
            for (var x = 0; x < w; x++)
            {
                for (var y = 0; y < h; y++)
                {
                    var color = sKBitmap.GetPixel(x, y);
                    bytes[byteIndex + 0] = color.Red;
                    bytes[byteIndex + 1] = color.Green;
                    bytes[byteIndex + 2] = color.Blue;
                    byteIndex += 3;
                }
            }
            sKBitmap.Dispose();

            var qRCodeReader = new ZXing.QrCode.QRCodeReader();
            var rGBLuminanceSource = new ZXing.RGBLuminanceSource(bytes, w, h);
            var hybridBinarizer = new ZXing.Common.HybridBinarizer(rGBLuminanceSource);
            var binaryBitmap = new ZXing.BinaryBitmap(hybridBinarizer);
            var hints = new Dictionary<ZXing.DecodeHintType, object>();
            hints.Add(ZXing.DecodeHintType.CHARACTER_SET, "utf-8");
            var result = qRCodeReader.decode(binaryBitmap, hints);

            return result != null ? result.Text : "";
        }
    }
}
