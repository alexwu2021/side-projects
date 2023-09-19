package com.xyz.ims.domain.bizlogic;

import com.xyz.ims.context.SpringContext;
import com.xyz.ims.service.MeasuredOperationService;

public class OutputToFile {
    protected MeasuredOperationService getOperationService() {
        return SpringContext.getBean(MeasuredOperationService.class);
    }
}
