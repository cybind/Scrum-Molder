using System;
using System.Collections.Generic;
using System.Data.Entity;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Data
{
    public class ScrumMolderContextInitializer : DropCreateDatabaseIfModelChanges<ScrumMolderContext>
    {
        protected override void Seed(ScrumMolderContext context)
        {
           //create sample data and attach it to the context.
           //         new List<Blog>
           //{
           // new Blog { BloggerName = "Julie", Title = "My Code First Blog",
           //            BlogDetail=new BlogDetails{Description="All about code first",
           //                            DateCreated=new System.DateTime(2011,3,1)}
           //           ,Posts=new List<Post>{
           //               new Post {
           //                 Title="ForeignKeyAttribute Annotation",
           //                 DateCreated=System.DateTime.Now, 
           //                 Content="Mark navigation property with ForeignKey"}}
           //          },
           // new Blog { BloggerName = "Ingemaar", Title = "My Life as a Blog",
           //             BlogDetail=new BlogDetails{
           //                             Description="Random tidbits",
           //                             DateCreated=new System.DateTime(2011,1,1)}},
           // new Blog { BloggerName = "Sampson", Title = "Tweeting for Dogs",
           //             BlogDetail=new BlogDetails{
           //                Description="How to tweet without opposable thumbs",
           //                DateCreated=new System.DateTime(2011,2,1)}}
           //}.ForEach(b => context.Blogs.Add(b));

            new List<Company>
                {
                    new Company
                    {
                        Id = Guid.NewGuid(),
                        Name = "Jaybird",
                        ChildCompanies = null,
                        Clients = new List<Client>
                        {
                            new Client
                            {
                                Id = Guid.NewGuid(),
                                Name = "Qwest",
                                Projects = new List<Project>
                                {
                                    new Project
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Connect Web"
                                    },
                                    new Project
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Connect Tablet"
                                    }
                                }
                            },
                            new Client
                            {
                                Id = Guid.NewGuid(),
                                Name = "eCreek",
                                Projects = new List<Project>
                                {
                                    new Project
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "eWOC"
                                    }
                                }
                            },
                            new Client
                            {
                                Id = Guid.NewGuid(),
                                Name = "Phill Izenson (Qwest)",
                                Projects = new List<Project>
                                {
                                    new Project
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Century Link Test Web"
                                    }
                                }
                            },
                            new Client
                            {
                                Id = Guid.NewGuid(),
                                Name = "Screen and Rend",
                                Projects = new List<Project>
                                {
                                    new Project
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Screen and Rent"
                                    }
                                }
                            }
                        }
                    }
                }.ForEach(s => context.Companies.Add(s));

            base.Seed(context);
        }
    }
}
