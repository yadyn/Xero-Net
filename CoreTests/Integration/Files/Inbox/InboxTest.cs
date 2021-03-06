using System;
using System.Threading.Tasks;
using CoreTests.Integration.Files.Support;
using NUnit.Framework;
using Xero.Api.Core.Model;
using File = Xero.Api.Core.Model.File;

namespace CoreTests.Integration.Files.Inbox
{
    [TestFixture]
    public class InboxTest : FilesTest
    {
        [Test]
        public async Task can_get_the_inbox_like_this()
        {
            var inbox = await Api.Inbox.GetInboxFolderAsync();

            Assert.IsTrue(inbox.Name == "Inbox");

            Assert.IsTrue(inbox.IsInbox);
        }

        [Test]
        public async Task can_add_a_file_to_inbox_like_this()
        {
            var filename = "Inbox file " + Guid.NewGuid() + ".png";

            var result = await Api.Inbox.AddAsync(create_file_with_name(filename), exampleFile);

            var file = await Api.Files.FindAsync(result.Id);

            Assert.IsTrue(file.Mimetype == "image/png");
            Assert.IsTrue(file.Name == filename);
        }

        [Test]
        public async Task can_remove_a_file_like_this()
        {
            var inboxId = (await Api.Inbox.GetInboxFolderAsync()).Id;

            var result = await Given_a_file_in(inboxId, "Test " + Guid.NewGuid() + ".png");

            await Api.Inbox.RemoveAsync(result);
            
            var notfound = await Api.Inbox.FindAsync(result);

            Assert.IsNull(notfound);
        }

        private File create_file_with_name(string filename)
        {
            return new Xero.Api.Core.Model.File()
            {
                Name = filename,
                FileName = filename,
                Mimetype = "image/png",
                User = new FilesUser()
                {
                    FirstName = "Bart",
                    LastName = "Simpson",
                    FullName = "Bart Simpson",
                    Name = "Bart@gmail.com"
                }
            };
        }
    }
}