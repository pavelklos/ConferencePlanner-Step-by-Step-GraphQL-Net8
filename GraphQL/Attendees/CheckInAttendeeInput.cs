using GraphQL.Data;

namespace GraphQL.Attendees
{
    public record CheckInAttendeeInput(
        [ID(nameof(Session))]
        int SessionId,
        [ID(nameof(Attendee))]
        int AttendeeId);
}