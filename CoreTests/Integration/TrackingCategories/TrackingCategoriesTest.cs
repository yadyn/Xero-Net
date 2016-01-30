using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;

namespace CoreTests.Integration.TrackingCategories
{
    public class TrackingCategoriesTest : ApiWrapperTest
    {
        private Invoice invoice_;
        public TrackingCategory category1_;
        public TrackingCategory category2_;
        private List<TrackingCategory> listresult_;

        public async Task Given_a_TrackingCategory_with_Options()
        {
            await Given_a_TrackingCategory();

            var option1 = Given_a_tracking_option();
            var option2 = Given_a_tracking_option();

            var optionCollection = await Api.TrackingCategories.GetOptionsByIDAsync(category1_.Id);

            await optionCollection.AddAsync(option1);
            await optionCollection.AddAsync(option2);

            category1_ = await Api.TrackingCategories.GetByIDAsync(category1_.Id);
        }

        public async Task Given_a_TrackingCategory_with_Option()
        {
            await Given_a_TrackingCategory();

            var option1 = Given_a_tracking_option();

            var optionCollection = await Api.TrackingCategories.GetOptionsByIDAsync(category1_.Id);

            await optionCollection.AddAsync(option1);

            category1_ = await Api.TrackingCategories.GetByIDAsync(category1_.Id);
        }

        public async Task Given_a_TrackingCategory()
        {
            category1_ = await Api.TrackingCategories.AddAsync(new TrackingCategory
            {
                Name = "TheJoker " + Guid.NewGuid(),
                Status = TrackingCategoryStatus.Active
            });

            Assert.IsTrue(category1_.Name.StartsWith("TheJoker"));

            Assert.IsTrue(category1_.Status == TrackingCategoryStatus.Active);
        }

        public async Task Given_two_TrackingCategorys()
        {
            category1_ = await Api.TrackingCategories.AddAsync(new TrackingCategory
            {
                Name = "TheJoker " + Guid.NewGuid(),
                Status = TrackingCategoryStatus.Active
            });

            category2_ = await Api.TrackingCategories.AddAsync(new TrackingCategory
            {
                Name = "HarleyQuinn " + Guid.NewGuid(),
                Status = TrackingCategoryStatus.Active
            });
        }

        public Option Given_a_tracking_option()
        {
            return new Option()
            {
                Id = Guid.Empty,
                Name = "Option " + Guid.NewGuid(),
                Status = TrackingOptionStatus.Active
            };
        }

        public List<Option> Given_a_tracking_options()
        {
            List<Option> options = new List<Option>();

            options.Add(Given_a_tracking_option());
            options.Add(Given_a_tracking_option());

            return options;
        }

        public async Task Given_name_change_to_categorie()
        {
            category1_.Name = "The Joker";

            var result = await Api.UpdateAsync(category1_);

            Assert.True(result.Name == "The Joker");
        }

        public async Task Given_approved_invoice_with_tracking_option(InvoiceType type = InvoiceType.AccountsPayable, InvoiceStatus status = InvoiceStatus.Draft)
        {
            Guid category = category1_.Id;
            string name = category1_.Name;
            string option = category1_.Options.FirstOrDefault().Name;

            var inv = await Api.CreateAsync(new Invoice
            {
                Contact = new Contact { Name = "Wayne Enterprises" },
                Type = type,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(90),
                LineAmountTypes = LineAmountType.Inclusive,
                Status = status,
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        AccountCode = "200",
                        Description = "Good value item",
                        LineAmount = 100m,
                        Tracking = new ItemTracking
                            {
                                new ItemTrackingCategory
                                {
                                    Id = category,
                                    Name = name,
                                    Option = option
                                }
                            }
                    }
                }

            });

            inv.Status = InvoiceStatus.Authorised;
            invoice_ = await Api.UpdateAsync(inv);
        }

        public Task<Invoice> Given_Invoice_is_voided()
        {
            invoice_.Status = InvoiceStatus.Voided;
            return Api.UpdateAsync(invoice_);
        }

        public async Task Given_Tracking_Category_is_deleted()
        {
            category1_ = await Api.TrackingCategories.DeleteAsync(category1_);

            Assert.True(category1_.Status == TrackingCategoryStatus.Deleted);
        }

        public async Task Given_both_Tracking_Category_is_deleted()
        {
            await Given_Tracking_Category_is_deleted();

            category2_ = await Api.TrackingCategories.DeleteAsync(category2_);

            Assert.True(category2_.Status == TrackingCategoryStatus.Deleted);
        }

        public async Task Given_Tracking_CategoryOption_is_deleted()
        {
            var result = await Api.TrackingCategories.DeleteTrackingOptionAsync(category1_, category1_.Options.FirstOrDefault());

            Assert.IsTrue(result.Status == TrackingOptionStatus.Deleted);
        }

        public async Task Given_first_Option_is_Archived()
        {
            category1_.Options.FirstOrDefault().Status = TrackingOptionStatus.Archived;

            var result = await (await Api.TrackingCategories.GetOptionsByIDAsync(category1_.Id)).UpdateOptionAsync(category1_.Options.FirstOrDefault());

            Assert.True(result.Status == TrackingOptionStatus.Archived);
        }

        public async Task Given_Tracking_Category_is_Archived()
        {
            category1_.Status = TrackingCategoryStatus.Archived;

            category1_ = await Api.TrackingCategories.UpdateAsync(category1_);

            Assert.True(category1_.Status == TrackingCategoryStatus.Archived);
        }

        public async Task Given_first_Option_Name_change()
        {
            var option = category1_.Options.FirstOrDefault();

            option.Name = "Mr Freeze";

            option = await (await Api.TrackingCategories.GetOptionsByIDAsync(category1_.Id)).UpdateOptionAsync(option);

            Assert.True(option.Name == "Mr Freeze");
        }

        public async Task Then_Category_Has_Option()
        {
            var result = await Api.TrackingCategories.GetOptionsByIDAsync(category1_.Id);

            Assert.True(result != null);

            //            Assert.True(result._trackingCat.Options.Any(i => i.Name == options.FirstOrDefault().Name));
            //            Assert.True(result._trackingCat.Options.Any(i => i.Name == options.Last().Name));
        }

        public async Task Then_Category_Has_Options()
        {
            var result = await Api.TrackingCategories.GetByIDAsync(category1_.Id);

            Assert.True(result != null);
            Assert.True(result.Options.Count() == 2);

            //            Assert.True(result._trackingCat.Options.Any(i => i.Name == options.FirstOrDefault().Name));
            //            Assert.True(result._trackingCat.Options.Any(i => i.Name == options.Last().Name));
        }

        public async Task Given_GetAll()
        {
            listresult_ = await Api.TrackingCategories.GetAllAsync();
        }

        public async Task Given_GetAll_with_Archived()
        {
            var api = Api.TrackingCategories.IncludeArchived(true);

            listresult_ = await api.GetAllAsync();
        }

        public void Then_Archieved_Tracking_Category_is_in_list()
        {
            var actualTracking = listresult_.SingleOrDefault(i => i.Id == category1_.Id);

            Assert.IsTrue(actualTracking != null);
        }

        public void List_contains_the_two_Tracking_Category()
        {
            Assert.IsTrue(listresult_.First().Name == category1_.Name);
            Assert.IsTrue(listresult_.Last().Name == category2_.Name);
        }
    }
}
