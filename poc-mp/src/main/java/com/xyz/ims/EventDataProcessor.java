package com.xyz.ims;

import com.xyz.ims.cache.ReferenceDataManager;
import com.xyz.ims.constant.ImsConstant;
import com.xyz.ims.context.SpringContext;
import com.xyz.ims.domain.Fact;
import com.xyz.ims.domain.model.LogNotificationEmailView;
import com.xyz.ims.service.MeasuredOperationService;
import com.xyz.ims.util.FactUtil;
import com.xyz.ims.util.FileUtil;
import com.xyz.ims.util.FileWriterPool;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Map;

public class EventDataProcessor {
    private String _outputContainingFolder = FileUtil.getAbsoluteFilePath(ImsConstant.FACT_FILE_OUTPUT_FOLDER);
    private FileWriterPool _fileWriterPool;

    public EventDataProcessor() throws FileNotFoundException {
        _fileWriterPool = new FileWriterPool();
    }

    private static EventDataProcessor me;

    static {
        try {
            me = new EventDataProcessor();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

    public static EventDataProcessor me(){
        return me;
    }

    public void processingEventData(LogNotificationEmailView lnev) {
        System.out.println("processing kafka event data, [" + lnev + "]");
        parseEventDataFileAndWriteToFactFileAndUpdateDw(lnev);
    }


    /**
     *
     *      * EMAIL_TO_UID, VIEW_DATE, NOTIFICATION_EMAIL_ID, CATEGORY, NOTIFICATION_ID
     *
     * @param lnev
     */
    private void parseEventDataFileAndWriteToFactFileAndUpdateDw(LogNotificationEmailView lnev) {
        Map<Long, Long> mp = ReferenceDataManager.me().getUserSeqToUserIdMap();
        if(mp == null){
            System.out.println("Error: unable to process LogNotificationEmailView [" + lnev + "] due to the absence of UserSeqToUserIdMap");
            return;
        }
        String createdDateFactFilePath =  this._outputContainingFolder + lnev.getViewDate() + ".fac";
        try {
            String repr = "email" + "_" + "open";
            Integer measuredOperationId  = getOperationService().getMappedValueByMeasuredOperationRepr(repr);
            Fact fact = FactUtil.convertToFact(lnev.getEmailToUid(), measuredOperationId);
            this._fileWriterPool.getWriter(new File(createdDateFactFilePath)).write(fact.toString() + "\n");
            System.out.println("parsed and wrote to fact file " + createdDateFactFilePath);
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            System.out.println("done parseEventDataFileAndWriteToFactFileAndUpdateDw()\n");
        }
    }

    protected MeasuredOperationService getOperationService() {
        return SpringContext.getBean(MeasuredOperationService.class);
    }
}
