namespace Domain.Constants;
public class LocationConstants
{
    public static string RootLocation = Path.Join(Directory.GetCurrentDirectory(), "wwwroot");
    public static string CourseLocation = Path.Join(RootLocation, "courses");
    public static string TeacherLocation = Path.Join(RootLocation, "teachers");
    public static string ProfileLocation = Path.Join(RootLocation, "profiles");
}
