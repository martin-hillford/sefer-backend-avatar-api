#!/bin/bash
dotnet restore
dotnet build --no-restore 
dotnet publish Sefer.Backend.Avatar.Api.csproj --output ./build