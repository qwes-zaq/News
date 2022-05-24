using News.Web.UI.Areas.Admin.Models.ViewModels.PostVMs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.UI.Areas.Admin.Models.Attributes
{
    public class PostTranslationListAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            

            if (value is List<PostTranslationVM> list)
            {
                bool hasOneTranslation = false;
                bool isTitleNull;
                bool isBodyNull;

                foreach (var i in list)
                {
                    isBodyNull = i.Body == null;
                    isTitleNull = i.Title == null;

                    if (!hasOneTranslation && i.Title != null && i.Body != null)
                    {
                        hasOneTranslation = true;
                    }

                    if (isTitleNull && !isBodyNull)
                    {
                        ErrorMessage = "Translation hasn't title";
                        return false;
                    }

                    if (!isTitleNull && isBodyNull)
                    {
                        ErrorMessage = "Translation hasn't body";
                        return false;
                    }
                }

                if (hasOneTranslation)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Is empty";
                    return false;
                }
            }

            ErrorMessage = "--- Error ---";
            return false;
        }
    }
}
