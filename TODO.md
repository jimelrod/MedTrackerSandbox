# TODO List

## Sql Exceptions (500) => 400

* Need custom exceptions

## Implement Concurrency Check

So, I'm thinking we can add a property to the DTOs that has a timestamp for the date retrieved.

We then add a filter when checking the resource, and if the modified date is after that DTO timestamp,
We send back a message asking them if they want to update their data on the page.
