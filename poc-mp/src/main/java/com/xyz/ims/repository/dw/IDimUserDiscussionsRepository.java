package com.xyz.ims.repository.dw;


import com.xyz.ims.db.jpa.DimDiscussionsEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface IDimUserDiscussionsRepository extends JpaRepository<DimDiscussionsEntity, Integer> {
}

