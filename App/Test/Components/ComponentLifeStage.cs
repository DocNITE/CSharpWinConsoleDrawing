namespace App.Test.Components;

public enum ComponentLifeStage : byte {
    Starting,
    Initializing,
    Running,
    Removing,
    Deleted,
}