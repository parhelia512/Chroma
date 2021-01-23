using Chroma.Natives.SDL;

namespace Chroma.Graphics
{
    public enum BlendingMode
    {
        None = SDL2.SDL_BlendMode.SDL_BLENDMODE_NONE,
        Add = SDL2.SDL_BlendMode.SDL_BLENDMODE_ADD,
        Modulate = SDL2.SDL_BlendMode.SDL_BLENDMODE_MOD,
        Multiply = SDL2.SDL_BlendMode.SDL_BLENDMODE_MUL,
        AlphaBlend = SDL2.SDL_BlendMode.SDL_BLENDMODE_BLEND
    }
}