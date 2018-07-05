# Time Keeping

I was tired to input my work time in spreadsheets that didn't work property so I implemented my time keeping application

A simple command based console application with localDB to save entry and exit notes.

### How it works

Use the following commands to save your time note.

```
entry //Saves the current date in a input type record
exit  //Saves the current date in a exit type record
log   //Shows the saved records 
help  //Shows the command list
clear //Clears all saved records
```

### Example
```
Enter a command: help
All commands:
entry
exit
log
help
clear

Enter a command: entry
Entry Registered

Enter a command: exit
Exit Registered

Enter a command: entry
Entry Registered

Enter a command: exit
Exit Registered

Enter a command: log
All TimeRecords from data base:

15 Exit - 05/07/2018 01:35:06
14 Entry - 05/07/2018 01:35:04
13 Exit - 05/07/2018 01:35:03
12 Entry - 05/07/2018 01:34:57

Enter a command: clear
All records was cleaned up
```
