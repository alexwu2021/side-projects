package com.xyz.ims.util;

import com.xyz.ims.domain.Fact;
import com.xyz.ims.setting.MeasuredOperation;

public class FactUtil {
    public static Fact convertToFact(Long userId, Integer measuredOperationId){
        MeasuredOperation measuredOperation = new MeasuredOperation(measuredOperationId);
        return new Fact(userId, measuredOperation);
    }
}
