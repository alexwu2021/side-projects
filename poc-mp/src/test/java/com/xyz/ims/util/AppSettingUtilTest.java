package com.xyz.ims.util;

import com.fasterxml.jackson.core.JsonProcessingException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

class AppSettingUtilTest {

    @Test
    void getAppSettings() throws JsonProcessingException {
        com.xyz.ims.setting.AppSettings appSettings = AppSettingUtil.getAppSettings();

        Assertions.assertTrue(appSettings != null && appSettings.getOperationSettings().stream().count() >= 2);
    }
}