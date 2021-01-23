using Chroma.Natives.SDL;

namespace Chroma.Graphics
{
    public enum PixelFormat : uint
    {
        RGB = SDL2.SDL_PIXFMT_RGB888,
        BGR = SDL2.SDL_PIXFMT_BGR888,
        BGRA = SDL2.SDL_PIXFMT_BGRA8888,
        RGBA = SDL2.SDL_PIXFMT_RGBA8888,
        ABGR = SDL2.SDL_PIXFMT_ABGR8888,
        ARGB = SDL2.SDL_PIXFMT_ARGB8888
    }
}