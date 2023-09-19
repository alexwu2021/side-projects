package com.xyz.ims.domain.model;

import com.xyz.ims.util.MathUtil;

public class ImsUser {
    private long id;
    public long getId(){
        return MathUtil.encodeForEntity(this.ctxTypeId, this.ctxId);
    }


    private int ctxTypeId;
    public int getCtxTypeId(){
        return this.ctxTypeId;
    }
    public void setCtxTypeId(int val){
        this.ctxTypeId = val;
    }

    private int ctxId;
    public int getCtxId(){
        return this.ctxId;
    }
    public void setCtxId(int val){
        this.ctxId = val;
    }
}
