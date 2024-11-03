# TODO List

## Required for Alpha
- [ ] Database Implemented
- [ ] Search Implemented
- [ ] Managed Libraries Implemented
- [ ] Open Projects Implemented
- [ ] Tag Creation and Tag Management 100% Implemented
  - [ ] Proper Parent/Child Relationship
  - [ ] Adding/Removing Parent/Child Relationships on existing tags
  - [ ] Removing Parent/Child Relationships on tag deletion.

- [X] Tagging Files 100% Implemented
- [X] Basic File Type Preview 100% Implemented

## Broad Features
- [ ] Implement Database
- [ ] Implement EXIF Data Saving (where possible)

## UX Features
- [ ] Search
  - [ ] Actually implement searching in main window
  - [ ] Actually implement searching when adding tags

## Entries  
- [ ] Actually allow loading from folders
- [ ] Implement ZipFile handling features
- [ ] Show common Metadata

- [ ] Metadata
  - **NOTE:** I'm not actually sure how to best implement Metadata here. Either use TagStudio's method or come up with
    something better. 

- [ ] Preview
  - [X] Basic File Types
    - [X] Make Images Display a Preview
    - [X] Make Text Files display a Preview
   
  - [ ] Better Support
    - [ ] Make GIF files actually animated
    
  - [ ] Additional File Types
    - [ ] LDtk preview support (actually render/display LDtk files)
    - [ ] aesprite preview support
    - [ ] Inochi2D preview support
    - [ ] GLB preview support
    - [ ] VRM preview support
    - [ ] Additional 3D Model Preview Support
    
  - [ ] Other Preview Support
    - [ ] Allow for listening to a few seconds of audio and show waveform as preview
    - [ ] Allow for viewing video file

- [X] Tagging
  - [X] Tag Files
  - [X] Untag Files
  
- [ ] Projects
  - [ ] Add entry to project
  - [ ] Remove entry to project
  
- [ ] Libraries
  - [ ] Add entry to Library
  - [ ] Remove entry from Library

- [ ] Licenses
  - [ ] Create License Entries
  - [ ] Remove License Entries
  - [ ] Add Licenses to other Entries
  - [ ] Add License Compatibility

## Tag Management
- [X] Tag Creation
  - [X] Allow for setting names
  - [X] Allow for setting parents

- [ ] Tag Management
  - [ ] Edit Tags
  - [X] Remove Tags
    - [X] Remove Tags from Entries that have the tag upon removal.
    - [ ] Remove Tags from Parents that have the tag upon removal.
      - **Note:** When removing tags that have children, should the children be reparented, or just be left 'floating'?

- [ ] Allow Tag Colors to be added (Requires Rework of Tag Relationships in UI)
- [ ] Display Parent(s) for Tags
  - **Note:** Tag Studio only displays the first parent, not any subsequent parents.

## Asset/File Management
- [ ] Implement Libraries
  - [ ] Implement Managed Library
  - [ ] Implement External Library
  - [ ] Implement Hybrid Library (Requires: Managed and External)

- [ ] Implement Projects
  - [ ] Implement Open Projects
  - [ ] Implement Closed Projects

- [ ] Implement Different Entry Types
  - [ ] Actually implement File Entries (License, File, etc.)

- [ ] Implement Licenses
  - [ ] Create License Entry Type