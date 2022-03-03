# Changelog

All notable changes to this project will be documented in this file. ​ The format is based
on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres
to [Semantic Versioning](https://semver.org/spec/v2.0.0.html). ​ ​

## [Unreleased]

### Added

### Changed

### Fixed

### Removed

## [1.0.1]

### Fixed

- Moved positioning of preprocessor in WebGL file so it compiles when not using the WebGL platform

## [1.0.0]

- Initial import from base repository

### Added

- WebGL support
  - New MonoBehaviour to receive file browser messages
  - Automatically create and maintain the flow of file io operations
- New interface for doing pure file IO
  - Specific platform file browser implementations wrap this when possible to simplify the functionality
- Wrap all code files in an assembly definition

### Changed

- Now use a helper class to retrieve the per-platform implementation rather than having the helper class do the implementation itself
- Moved around code to reduce multiple type declarations in one file
- Made all implementation classes internal
  - Interfaces are still public

### Fixed

- Add platform limitations for plugin source DLLs (i.e. only load linux plugin generator when building linux platform)

### Removed

- Sample code
