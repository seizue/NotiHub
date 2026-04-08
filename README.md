# NotiHub
Is a Event & Schedule Manager Desktop Application that enables users to schedule, track, and share events through a calendar-driven interface, supporting notifications, audit logging, multi-user access, and external integrations like iCalendar and dpaste.

## Default Credentials

A default administrator account is automatically created on first launch if no existing admin is found:

* **Username:** Admin
* **Password:** 1727

> ⚠️ For security reasons, it is highly recommended to change the default password after your first login.

Once logged in, users can change their password in **Settings**.  
Admins can also manage user accounts, including adding and updating multiple system users.

You can also modify the default admin credentials directly in the source code:
`LogIn.cs → EnsureDefaultAdmin()`

## Download
[Download](https://github.com/seizue/NotiHub/releases)

## Screenshot
<img width="1366" height="735" alt="explorer_ZrOT5TqAxr" src="https://github.com/user-attachments/assets/bbc3235f-6397-49bd-9c65-a0d1c30daa5e" />

## Features

- **Calendar** — Monthly, Weekly, List, Day view with event indicators, today highlight, and recurring event support
- **Event Scheduling** — Create events with time, location, status, priority, tags, and reminders
- **Notifications** — Desktop popups with snooze support; urgent events trigger a distinct alert
- **Import / Export `.ics`** — Import from and export to iCalendar files compatible with Google Calendar, Outlook, and Apple Calendar
- **Export CSV** — Export events and audit logs to CSV
- **Search** — Filter events by keyword, date range, tags, and priority
- **Audit Trail** — Full history of event actions with filtering and pagination
- **User Accounts** — Multi-user login, credential management, and configurable window settings
- **Share Notes (URL / QR Code)** — Generate secure, shareable links and QR codes via dpaste for quick cross-device access and seamless information sharing.

## Icons
Icons by <a target="_blank" href="https://icons8.com">Icons8</a>
