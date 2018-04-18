

namespace QuestionnaireXForms.Domain
{

    public enum AttachmentType
    {
        None=0,
        Base64=1,
        Jpeg=2,
        Png=3
    }

    /*
    public static AttachmentType fromString(String str)
        {
        if ("NONE".equals(str))
        return NONE;
        else if ("BASE64".equals(str))
        return BASE64;
        else if ("JPEG".equals(str))
        return JPEG;
        else if ("PNG".equals(str))
        return PNG;
        return NONE;
    }

    public String toString()
    {
    switch (this)
    {
    case NONE:
    return "NONE";
    case BASE64:
    return "BASE64";
    case JPEG:
    return "JPEG";
    case PNG:
    return "PNG";
    default:
    return "NONE";
}
}
}*/
    public class Attachment 
    {
        public AttachmentType type;
        public string fileName;
        
        
    }
}