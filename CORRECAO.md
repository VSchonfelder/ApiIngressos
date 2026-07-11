# Correção AV2 — ApiIngressos

| # | Item de Avaliação | Nota | Justificativa |
|---|-------------------|:----:|---------------|
| 01 | Padrão AAA nos Testes | 0,5 | 3 métodos com `// Arrange`, `// Act`, `// Assert` (EventosTests, UsuariosRegrasTests, UsuariosTests) |
| 02 | Nomenclatura e Independência | 0,5 | Métodos seguem `Metodo_Cenario_ResultadoEsperado`; zero condicionais |
| 03 | Padrões Arquiteturais | 0,5 | 3 cenários com `Positivo:`/`Negativo:` em `/docs/analise_arquitetura.md` |
| 04 | Violações Arquiteturais | 0,5 | 5 violações com `**Problema:**`, `**Evidência:**`, `**Impacto:**`, `**Ação Recomendada:**` |
| 05 | ADR | 0,5 | `001-escolha-do-micro-orm.md` com Contexto, Decisão, Consequências, Status: Aceito, Prós/Contras |
| 06 | Dívida Técnica | 0,5 | 6 dívidas com colunas ID, Descrição, Freq. Alteração, Risco, Esforço, Decisão (Alto/Médio/Baixo) |
| 07 | Priorização Dívida | 0,5 | P1 (DT-001, DT-002), P2 (DT-003, DT-004), P3 (DT-005, DT-006) |
| 08 | Classificação Manutenção | 0,5 | 12 tickets classificados: Corretiva, Adaptativa, Perfectiva, Preventiva |
| 09 | Pipeline de Liberação | 0,5 | 4 passos: Análise de Impacto, Teste Cirúrgico, Feature Toggle, Estratégia de Release |
| 10 | Plano de Iteração | 0,5 | Objetivo, Escopo, Entregáveis, Risco Principal, DoD preenchidos |
| 11 | Quadro Kanban e WIP | 0,5 | 4 colunas + WIP máximo = 3 (<= 4 integrantes) |
| 12 | Matriz de Riscos | 0,5 | 5 riscos com Probabilidade, Impacto, Estratégia, Ação Planejada |
| 13 | Gatilhos de Risco | 0,5 | Todos 5 gatilhos com >=20 caracteres |
| 14 | Métrica DORA | 0,5 | "Lead Time for Changes" com 7 campos completos |
| 15 | Métrica de Qualidade | 0,5 | "Change Failure Rate" com 7 campos completos |
| 16 | SLO | 0,5 | SLI, Fórmula, Fonte, Janela (30 dias), Alvo (99.5%) |
| 17 | Error Budget Policy | 0,5 | 3 níveis; Nível 3 com "Feature Freeze" e "Zero novas funcionalidades" |
| 18 | Segurança SSDF | 0,5 | Nenhuma credencial hardcoded nos `.cs` do `/src` |
| 19 | Threat Model e Gates | 0,5 | Ativos, Vetor, Falha, Mitigação + Gate 1/2/3 (SAST, SCA, DAST) |
| 20 | Topologia Times e DoD | 0,5 | 4 tipos Team Topologies + release_checklist com 7 `[x]` |

**Nota Final: 10,0 / 10,0**
