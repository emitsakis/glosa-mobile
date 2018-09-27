﻿#!/usr/bin/env bash

echo "Started pre-build script"

if [ ! -n "$AZURE_MOBILE_SERVICE_CLIENT_URI" ]
then
    echo "You need define the API_URL variable in App Center"
    exit
else
	echo "**pre-build $AZURE_MOBILE_SERVICE_CLIENT_URI"
fi

echo "$APPCENTER_SOURCE_DIRECTORY"

APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/GreenLight.Core/Constants.cs

if [ -e "$APP_CONSTANT_FILE" ]
then
    echo "Updating values $AZURE_MOBILE_SERVICE_CLIENT_URI in Constants.cs"
    sed -i '' 's#AZURE_MOBILE_SERVICE_CLIENT_URI = "[a-z:./]*"#AZURE_MOBILE_SERVICE_CLIENT_URI = "'$AZURE_MOBILE_SERVICE_CLIENT_URI'"#' $APP_CONSTANT_FILE
	sed -i '' 's#API_GLOSA_MAP_ENDPPOINT_URL = "[a-z:./]*"#API_GLOSA_MAP_ENDPPOINT_URL = "'$API_GLOSA_MAP_ENDPPOINT_URL'"#' $APP_CONSTANT_FILE
	sed -i '' 's#API_GLOSA_SPAT_ENDPPOINT_URL = "[a-z:./]*"#API_GLOSA_SPAT_ENDPPOINT_URL = "'$API_GLOSA_SPAT_ENDPPOINT_URL'"#' $APP_CONSTANT_FILE
	sed -i '' 's#API_GLOSA_CAM_ENDPPOINT_URL = "[a-z:./]*"#API_GLOSA_CAM_ENDPPOINT_URL = "'$API_GLOSA_CAM_ENDPPOINT_URL'"#' $APP_CONSTANT_FILE
	sed -i '' 's#ApiUrl = "[a-z:./]*"#AZURE_APP_CENTER_IOS_KEY = "'$AZURE_APP_CENTER_IOS_KEY'"#' $APP_CONSTANT_FILE
	sed -i '' 's#ApiUrl = "[a-z:./]*"#AZURE_APP_CENTER_ANDROID_KEY = "'$AZURE_APP_CENTER_ANDROID_KEY'"#' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi