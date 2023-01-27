# RuneSharper

RuneSharper is an Stat tracking platform for Old School Runescape.

# Components

There are 3 main components to RuneSharper. These are:
1. API - Utilitised by the front-end to fetch data
2. Client - Angular 14 app to display stats to users
3. Worker - Background process currently used to poll the OSRS API to fetch and store data

# Containerization

Currently, only the Worker service is containerized, but I'm working on doing the same for the API and Client. Utilising docker-compose to launch the entire application could be a potential option.

I'm also in the process of utilising containers within integration tests, to spin up background sql server instances to run clean, isolated tests.

# Notes

RuneSharper is in very early stages, and needs a lot of work (particularly on the front-end). However, the worker service is in a state to allow it to fetch and store data in a reliable format.
