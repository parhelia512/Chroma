namespace Chroma.Natives.FreeType.Native
{
    internal enum FT_Orientation
    {
        FT_ORIENTATION_TRUETYPE = 0,
        FT_ORIENTATION_POSTSCRIPT = 1,
        FT_ORIENTATION_FILL_RIGHT = FT_ORIENTATION_TRUETYPE,
        FT_ORIENTATION_FILL_LEFT = FT_ORIENTATION_POSTSCRIPT,
        FT_ORIENTATION_NONE
    }
}