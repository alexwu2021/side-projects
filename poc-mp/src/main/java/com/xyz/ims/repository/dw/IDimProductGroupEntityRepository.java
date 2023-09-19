package com.xyz.ims.repository.dw;


import com.xyz.ims.db.jpa.DimProductGroupsEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface IDimProductGroupEntityRepository extends JpaRepository<DimProductGroupsEntity, Integer> {
}

