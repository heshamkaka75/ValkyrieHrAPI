namespace ValkyrieHr.Installer
{
    public static class SetupMiddlewarePipeline
    {
        public static WebApplication SetupMiddleware(this WebApplication app)
        {
            // Configure the pipeline - ORDER MATTERS.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseCors("front");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
