package com.xyz.ims.repository.dw;


import com.xyz.ims.db.jpa.DimMessageContextEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface IDimMessageContextEntityRepository extends JpaRepository<DimMessageContextEntity, Integer> {
}

