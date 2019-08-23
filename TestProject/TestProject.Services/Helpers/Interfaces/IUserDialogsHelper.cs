using System.Threading.Tasks;

using TestProject.Services.Enums;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IUserDialogsHelper : IDialogsHelper
    {
        Task<EditPhotoDialogResult> ChoosePhotoEditOption();
    }
}
