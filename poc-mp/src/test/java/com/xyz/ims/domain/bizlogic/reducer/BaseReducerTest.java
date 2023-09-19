package com.xyz.ims.domain.bizlogic.reducer;

import com.xyz.ims.domain.model.ProcessingErrorCodeEnum;
import com.xyz.ims.domain.model.ProcessingResult;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

class BaseReducerTest {

    @Test
    void getReducerFile() {
    }

    @Test
    void doReductionFor() {
        BaseReducer baseReducer = new BaseReducer(20200101, 20200701);
        ProcessingResult processingResult = baseReducer.doReductionFor();
        Assertions.assertTrue(processingResult.getErrorCode() == ProcessingErrorCodeEnum.success);


    }


}