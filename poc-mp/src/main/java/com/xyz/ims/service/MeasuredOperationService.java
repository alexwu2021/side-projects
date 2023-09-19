package com.xyz.ims.service;

import com.xyz.ims.setting.MeasuredOperation;
import com.xyz.ims.setting.MeasuredOperationManager;
import org.springframework.stereotype.Service;

@Service
public class MeasuredOperationService implements IMeasuredOperationService {

    private MeasuredOperationManager _measureOperationManager;



    public MeasuredOperationService(){
        _measureOperationManager = MeasuredOperationManager.me();
    }

    @Override
    public Integer getMappedValueByMeasuredOperation(MeasuredOperation measuredOperation)  {
        return _measureOperationManager.getMeasuredOperationReprToValueMap().getOrDefault(measuredOperation.getRepr(), 0);
    }

    @Override
    public Integer getMappedValueByMeasuredOperationRepr(String repr) {
        return _measureOperationManager.getMeasuredOperationReprToValueMap().getOrDefault(repr, 0);
    }


}
