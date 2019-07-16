using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using MvvmCross.Commands;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : BaseViewModel
    {
        private readonly IBaseRepository<TodoItem> _genericTodoItemRepository;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                RaisePropertyChanged(() => IsDone);
            }
        }

        public CreateTodoItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _genericTodoItemRepository = new BaseRepository<TodoItem>();

            BackToListCommand = new MvxAsyncCommand(GoBack);
            CreateTodoItemCommand = new MvxAsyncCommand(CreateItem);
        }
        
        public IMvxAsyncCommand CreateTodoItemCommand { get; private set; }

        public IMvxAsyncCommand BackToListCommand { get; private set; }

        private async Task GoBack()
        {
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }

        private async Task CreateItem()
        {
            TodoItem item = new TodoItem { Name = this.Name, Description = this.Description, IsDone = this.IsDone };
            await _genericTodoItemRepository.Insert(item);
            StaticObjects.TodoItems.Add(item);
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
