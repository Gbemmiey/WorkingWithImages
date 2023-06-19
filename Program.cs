using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

string imagesFolder = Path.Combine(
    Environment.CurrentDirectory, "images");

WriteLine($"I will look for images in {imagesFolder}");
WriteLine();


if (!Directory.Exists( imagesFolder))
{
    WriteLine();
    WriteLine("Folder does not exist!");
    return;
}


IEnumerable<string> images = Directory.EnumerateFiles(imagesFolder);
foreach (string imagePath in images)
{
    if (Path.GetFileNameWithoutExtension(imagePath).EndsWith("-thumbnail"))
    {
        WriteLine($"Skipping: \n {imagePath}");
        WriteLine();
        continue;
    }
    string thumbnailPath =  Path.Combine(
        Environment.CurrentDirectory, "images",
        Path.GetFileNameWithoutExtension (imagePath) + "-thumbnail" 
        + Path.GetExtension(imagePath));

    using (Image image = Image.Load(imagePath))
    {
        WriteLine($"Converting: \n {imagePath}");
        WriteLine($"To: \n {thumbnailPath}");

        image.Mutate(x => x.Resize(image.Width / 10, image.Height / 10));
        image.Mutate(x => x.Grayscale());
        image.Save(thumbnailPath);
        WriteLine();
    }
}

WriteLine("Image Processing complete. View the images folder");
