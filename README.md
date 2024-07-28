# Penumbra's Succor

## Description

### A 2D Metroidvania proof-of-concept in Godot

## Templates

### Legal

#### Attribution

> When using assets that have any kind of legalization, we need to be consistent
> In an effort to be able to easily add accurate descriptions and keep things
> intentional, when adding a new asset pack or anything we'll use the following
> process.  We will use these new `_attr.json` files 

#### Template

1) Locate the appropriate Parent folder (Assets/..., Scripts/..., Scenes/..., etc.)
2) Add a new Folder in the parent folder with the name of the assetpack in camelCase
3) Paste the assetpack into the new folder
4) add a new file called `_attr.json`
5) Copy/Paste the following code into the new file and update the properties

```json
// _attr.json
{
    "url": "https://ragnapixel.itch.io/particle-fx",
    "license": "", // cc3, cc4, something like that, document here when you add a new license type
    "packName": "",
    "author": ""
}

```