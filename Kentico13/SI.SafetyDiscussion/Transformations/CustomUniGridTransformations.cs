using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.Membership;
using CMS.UIControls;
using System.Collections.Generic;

// Registers the custom module into the system
[assembly: RegisterModule(typeof(CustomUniGridTransformations))]

public class CustomUniGridTransformations : Module
{
    // Module class constructor, the system registers the module under the name "CustomUniGridTransformations"
    public CustomUniGridTransformations()
        : base("CustomUniGridTransformations")
    {
    }

    // Contains initialization code that is executed when the application starts
    protected override void OnInit()
    {
        base.OnInit();

        UniGridTransformations.Global.RegisterTransformation("#getColleagues", GetColleagues);
    }

    private static object GetColleagues(object parameter)
    {        
        var colleagueIDs = ValidationHelper.GetString(parameter, "");
        if (!string.IsNullOrEmpty(colleagueIDs))
        {
            var splitIDs = colleagueIDs.Split('|');
            if(splitIDs != null && splitIDs.Length > 0)
            {
                var colleagueNames = new List<string>();
                foreach(var colleagueID in splitIDs)
                {
                    if(int.TryParse(colleagueID, out int userId))
                    {
                        var userInfo = UserInfo.Provider.Get(userId);
                        if (userInfo != null && !string.IsNullOrEmpty(userInfo.FullName))
                        {
                            colleagueNames.Add(userInfo.FullName);
                        }
                    }
                }

                if(colleagueNames.Count > 0)
                {
                    return colleagueNames.Join(", ");
                }
            }
        }

        return parameter;
    }
}
