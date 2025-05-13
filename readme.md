# Sefer.Backend.Avatar.Api
The Avatar Api is a microservice of the Sefer Framework that provides functionality for managing user avatars. 
This service supports local avatar uploads and integrates with popular avatar services such as Gravatar and Libravatar, 
allowing users to easily manage their profile images.

## Features

- **Local Avatar Upload**: Users can upload their own avatar images directly to the application.
- **Gravatar Integration**: Automatically fetch and display user avatars from Gravatar based on their email address.
- **Libravatar Integration**: Support for fetching avatars from Libravatar, an open-source alternative to Gravatar.

Please note, the service will not connect to Gravatar or Libravatar be itself, the /gravatar can be used for that.

## Configuration
The web service necessitates the configuration of the following (environment) variables:

| Settings var   | Environment Var | Description                                                           |
|----------------|-----------------|-----------------------------------------------------------------------|
| Avatar.Store   | AVATAR_STORE    | The folder or the url to an Azure blob store where images are cached. |
| Avatar.UseBlob | AVATAR_USE_BLOB | If set to true, Azure blob storage will be used, else a local folder  |
| Avatar.ApiKey  | AVATAR_API_KEY  | The api to facilitated inner service communication.                   |

## License 
GPL-3.0-or-later