# TSC  

TSC is a Clone (of sorts) of [TagStudio](https://github.com/TagStudioDev/TagStudio) in Avalonia and C#. 

It's designed to be more geared towards asset management, than just organization.


## QnA
### Why do GIFs not play?  
Currently, Avalonia does not fully support GIFs, while there are ways to make them animate/play for the time being
I'm just not supporting it.

### Why does X File Format not have a preview?  
Currently, I've only implemented Previews for Images and Text Files as they were relatively easy to do. Adding in
support for more file formats will be done eventually.

### What can this do?  
Currently, nothing really.

You can start the application, you can click on things in your home directory, and you can create/add/remove tags.
These tags are not persisted yet. 

### Why not just use/contribute to TagStudio?  
TagStudio has several features that are nice, however it does have some drawbacks -- notably:

1. It's written in Python and uses Qt.  
This is not as much of an issue, however it does mean I can't contribute as much, as I am
unfamiliar with Qt entirely.

2. It's still *very* Alpha.
Notably you can not remove tags.

### Why did you make this?
Aside from the above, I wanted something that was a bit more Game-Development and more DAM focused, while retaining many
of the features and design of TagStudio, I also wanted something that was/is a bit more stable.

### When will this become alpha?  
Check the TODO.md file to see what's left for me to do before I feel comfortable calling it alpha!

### Why use .NET and Avalonia?  
Because I'm quite familiar with it, and I know XAML really well. I actually think XAML is not bad for designing and
creating UIs. It is not the prettiest, and there are some rough-edges, but.

### Why is this AGPL 3?  
It's AGPL 3 for a few reasons:

1. TagStudio (the program this was inspired by) is GPL 3

2. Due to TSC's structure and goals, I want to ensure that if this were to be put on a server
   that the community can always benefit from improvements made by others.

3. Going from More Restrictive to Less Restrictive Licenses are easier than the reverse. 