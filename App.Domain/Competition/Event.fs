module App.Domain.Competition.Event

open App.Domain.Competition
open App.Domain.Competition.Results.ResultObjects

[<Struct; CLIMutable>]
type CompetitionCreatedV1 =
    { CompetitionId: Id.Id
      StartlistId: Startlist.Id
      ResultsId: ResultsModule.Id }

[<Struct; CLIMutable>]
type CompetitionStartedV1 = { CompetitionId: Id.Id }

[<Struct; CLIMutable>]
type CompetitionRoundStartedV1 =
    { CompetitionId: Id.Id
      RoundIndex: int }

[<Struct; CLIMutable>]
type CompetitionRoundEndedV1 =
    { CompetitionId: Id.Id
      RoundIndex: int
      NextRoundIndex: int option }

[<Struct; CLIMutable>]
type CompetitionSuspendedV1 = { CompetitionId: Id.Id }

[<Struct; CLIMutable>]
type CompetitionContinuedV1 = { CompetitionId: Id.Id }

[<Struct; CLIMutable>]
type CompetitionCancelledV1 = { CompetitionId: Id.Id }

[<Struct; CLIMutable>]
type CompetitionEndedV1 = { CompetitionId: Id.Id }

// TODO: Przenieść to!
[<Struct; CLIMutable>]
type CompetitionJumpResultRegisteredV1 =
    { CompetitionId: Id.Id
      JumpResultId: JumpResult.Id }

// ---------- discriminated union + versioning ----------------------------------

type CompetitionEventPayload =
    | CompetitionCreatedV1 of CompetitionCreatedV1
    | CompetitionStartedV1 of CompetitionStartedV1
    | CompetitionRoundStartedV1 of CompetitionRoundStartedV1
    | CompetitionRoundEndedV1 of CompetitionRoundEndedV1
    | CompetitionSuspendedV1 of CompetitionSuspendedV1
    | CompetitionContinuedV1 of CompetitionContinuedV1
    | CompetitionCancelledV1 of CompetitionCancelledV1
    | CompetitionEndedV1 of CompetitionEndedV1
    | CompetitionJumpResultRegisteredV1 of CompetitionJumpResultRegisteredV1

module Versioning =
    let schemaVersion =
        function
        | CompetitionCreatedV1 _ -> 1us
        | CompetitionStartedV1 _ -> 1us
        | CompetitionRoundStartedV1 _ -> 1us
        | CompetitionRoundEndedV1 _ -> 1us
        | CompetitionSuspendedV1 _ -> 1us
        | CompetitionContinuedV1 _ -> 1us
        | CompetitionCancelledV1 _ -> 1us
        | CompetitionEndedV1 _ -> 1us
        | CompetitionJumpResultRegisteredV1 _ -> 1us
