# Togepi Backend

## Introduction
This is the ASP.NET part of Project Togepi.
Here, the reports are received from Togepi Assistant, organized and sent to the dispatcher dashboard.

## Togepi Backend flow
Togepi Assistant extracts the important data from conversing with the reporters, and sends their reports to the backend.
When a new event report is received, it is checked against the rest of the active events in the system.
If it is the same type and sent withing reasonable distance from the second event, they are combined and their reports appear together, thus saving the dispatchers from tending to the same event twice.

When the dashboard loads, it requests the list of active events from the backend, and shows them on the map.
When enterring a specific event, the backend send all of the related reports and they are shown on the dashboard.
