# Publish to Registry

# Installation
A powershell script is provider to publish the app to a registry.
The script takes the following parameters:
- `-tag` - The tag to publish with. Default: `dev`
- `-registry` - The registry to publish to. Default: `localhost:5000`
- `-arm64` - Publish for arm64 architecture. Default: `false`
- `-nobuild` - Skip the build step. Default: `false`
- `-push` - Push the image to the registry. Default: `false`

A default tag is provided for development purposes. This tag is not intended for production use.
A .env file could be created at the top level of the repository to set the registry.