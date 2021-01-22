# Monopoly (not Unity)
## What is this?
It's one more Monopoly clone made with C# and WPF (don't think about **PreUnity** in the name, just laugh at our hopes of transferring the ECS logic into Unity DOTS which never came true).

## Why?
This was a uni group project/coursework for OOP (which is funny, since there's barely any OOP left in here)

## Structure
The logic (which you can find in MonopolyPreUnity project) is built using the ECS ([Entity Component System](https://en.wikipedia.org/wiki/Entity_component_system)) paradigm. Its implementation here is pretty straightforward (look into the [MonopolyPreUnity.Entity](https://github.com/BardiTheWeird/MonopolyPreUnity/tree/master/MonopolyPreUnity/Entity) and [MonopolyPreUnity.Systems](https://github.com/BardiTheWeird/MonopolyPreUnity/tree/master/MonopolyPreUnity/Systems) to get the gist of it). Again, it's kinda funny, since ECS is a data-oriented paradigm, but OK.

The visual/interactive part was created using WPF framework, but with a complete disregard towards how you're actually supposed to use it. It's terrible and made in too much rush. Don't look at it. It might like an elephant in the room, but it's not an elephant you can't pet on the head.

## Theme
The one part which I think is actually fantastic is the theme of this Monopoly. Yeah-yeah, it's just a plain copy of a boring-ass game, but! it's themed after [our uni](https://en.wikipedia.org/wiki/Igor_Sikorsky_Kyiv_Polytechnic_Institute), and the cards' design is actually pretty slick.

*And no, buying faculty buildings is not strange at all.*
