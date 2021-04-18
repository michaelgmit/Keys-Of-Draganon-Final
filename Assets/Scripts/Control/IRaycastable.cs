namespace RPG.Control
{
    public interface IRaycastable
    {
        CursorType GetCursorType(); //tells teh raycaster which cursor to send back
        bool HandleRaycast(PlayerController callingController);
    }
}
