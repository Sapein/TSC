# TODO List

## Required for Alpha
- [ ] Database Implemented
- [ ] Managed Libraries Implemented
- [ ] Open Projects Implemented
- [ ] Implement Associated Tags
  - [ ] Associated Tags UI Work
  - [ ] Associated Tags Implementation Work
 
- [ ] Tag Creation and Tag Management 100% Implemented
  - [X] Proper Parent/Child Relationship
  - [X] Adding Parent/Child Relationships on Existing tags
  - [ ] Removing Parent/Child Relationships on Existing tags
  - [X] Removing Parent/Child Relationships on tag deletion.

- [X] Search Implemented
- [X] Tagging Files 100% Implemented
- [X] Basic File Type Preview 100% Implemented

## Broad Features
- [ ] Implement Database
- [ ] Implement EXIF Data Saving (where possible)
- [ ] Implement Advanced Search Mode

## UX Features
- [X] Search
  - [X] Actually implement searching in main window
  - [X] Actually implement searching when adding tags

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
  - [ ] Associated Tags
    - [ ] Add Associated Tags to Entry
    - [ ] Remove Associated Tag from Entry
    - [ ] Edit Associated Tags on Entry
    - [ ] Add Associated Tags UI
    - [ ] Edit Associated Tags UI
    - [ ] Remove Associated Tags UI
  
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
    - [X] Add Parents to Tag
    - [ ] Remove All Parents from Tag
    - [X] Remove Some Parents from Tag
  - [X] Remove Tags
    - [X] Remove Tags from Entries that have the tag upon removal.
    - [X] Remove Tags from Parents that have the tag upon removal.
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