using Chroma.Natives.SDL;

namespace Chroma.Graphics
{
    public enum TextureType
    {
        Static = SDL2.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC,
        Streaming = SDL2.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,
        RenderTarget = SDL2.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET
    }
}