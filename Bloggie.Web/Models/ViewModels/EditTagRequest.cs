﻿namespace Bloggie.Web;

public class EditTagRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
}
