FROM microsoft/dotnet:2.2-sdk AS builder

ENV APP_DIR=/app
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
ENV NUGET_XMLDOC_MODE=skip
RUN mkdir -p $APP_DIR

WORKDIR $APP_DIR
ADD . $APP_DIR
RUN dotnet build -c Release Bergendahls.Template.sln \
    && dotnet publish -c Release Bergendahls.Template.sln -o ./out

FROM microsoft/dotnet:2.2-runtime
WORKDIR /app
EXPOSE 5000
COPY --from=builder /app/Bergendahls.Template.Api/out .
ENTRYPOINT ["dotnet", "Bergendahls.Template.Api.dll"]
