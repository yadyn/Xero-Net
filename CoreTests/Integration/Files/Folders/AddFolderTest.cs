using System;
using System.Threading.Tasks;
using CoreTests.Integration.Files.Support;
using NUnit.Framework;

namespace CoreTests.Integration.Files.Folders
{
    public class AddFolderTest : FilesTest
    {
        [Test]
        public async Task can_create_a_folder_like_this()
        {
            var result = await Api.Folders.AddAsync("Test Folder" + Guid.NewGuid());
        }

        [Test]
        public async Task can_get_all_folders_like_this()
        {
            var allFolders = await Api.Folders.GetFoldersAsync();

            Assert.True(allFolders[0].Name == "Inbox");

            Assert.True(allFolders[1].Name == "Contracts");
        }

        [Test]
        public async Task can_remove_a_folder_like_this()
        {
            var folder = await Api.Folders.AddAsync("Test Folder" + Guid.NewGuid());

            await Api.Folders.RemoveAsync(folder.Id); // Hint ->folder is empty
        }
      }
}