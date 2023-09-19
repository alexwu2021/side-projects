package com.xyz.ims.service;

import com.xyz.ims.setting.MeasuredOperation;

public interface IMeasuredOperationService {
    Integer getMappedValueByMeasuredOperation(MeasuredOperation measuredOperation);

    Integer getMappedValueByMeasuredOperationRepr(String repr);
}
