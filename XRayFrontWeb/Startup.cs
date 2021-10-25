using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XRayFrontWeb.Configuration;

namespace XRayFrontWeb
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			ApiLayer.Instance = this.Configuration.GetSection("ApiLayer").Get<ApiLayer>();

			#region AWS configuration
			AwsConfig.Instance = this.Configuration.GetSection("Aws").Get<AwsConfig>();

			// X-Ray to automatically trace web requests and responses.
			app.UseXRay("XRayFrontWeb", this.Configuration);

			// Get all AWS SDK clients to auto subsegment requests to AWS services.
			AWSSDKHandler.RegisterXRayForAllServices();

			// OR selectively register which SDK clients trace with X-Ray.
			//AWSSDKHandler.RegisterXRay<Amazon.S3.IAmazonS3>();

			#endregion AWS configuration

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
						 name: "default",
						 pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
