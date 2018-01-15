namespace CanvasDesigner
{
    public static class AppConstants
    {
        
        public const  char BACKGROUND_COLOR = ' ';
        public const char VERTICLE_BOUNDRY = '|';
        public const char HORIZONTAL_BOUNDRY = '-';
        public const char LINE_COLOR = 'x';
        public const char RECTANGLE_COLOR = 'x';

        //public const string CREATE_CANVAS_COMMAND_PATTERN = @"^C(\s)+([0-9]+(\s)*){2}";
        
        public const string CREATE_CANVAS_COMMAND_PATTERN = @"^C[\s]*([+-]?[\s]*(?(\d{1,3},)(\d{1,3}(,\d{3})+)|\d+)[\s]*){2}$";
        public const string DRAW_LINE_COMMAND_PATTERN = @"^L[\s]*([+-]?[\s]*(?(\d{1,3},)(\d{1,3}(,\d{3})+)|\d+)[\s]*){4}$"; //@"^L(\s)+([0-9]+(\s)*){4}";
        public const string DRAW_RECTANGLE_COMMAND_PATTERN = @"^R[\s]*([+-]?[\s]*(?(\d{1,3},)(\d{1,3}(,\d{3})+)|\d+)[\s]*){4}$";
        public const string FILL_CONNECTED_AREA_COMMAND_PATTERN = @"^B(\s)+([0-9]+(\s)*){2}([A-z]+(\s)*)";
        public const string QUIT_COMMAND_PATTERN = "Q";
        public const string HELP_COMMAND_PATTERN = "HELP";
        public const string CLEAR_COMMAND_PATTERN = "CLEAR";

    }
}