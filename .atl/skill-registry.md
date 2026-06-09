# Skill Registry — api_seminco_4

<!-- Built by sdd-init. This registry is an index; SKILL.md remains the source of truth. -->

Last updated: 2026-06-09

## Sources scanned

### User skill directories

- `/Users/aldair/.config/opencode/skills/` — found skills
- `~/.pi/agent/skills/` — not present or no matching skills detected
- `~/.config/agents/skills/` — not present or no matching skills detected
- `~/.agents/skills/` — not present or no matching skills detected
- `~/.kimi/skills/` — not present or no matching skills detected
- `~/.config/kilo/skills/` — not present or no matching skills detected
- `~/.claude/skills/` — not present or no matching skills detected
- `~/.gemini/skills/` — not present or no matching skills detected
- `~/.gemini/antigravity/skills/` — not present or no matching skills detected
- `~/.cursor/skills/` — not present or no matching skills detected
- `~/.copilot/skills/` — not present or no matching skills detected
- `~/.codex/skills/` — not present or no matching skills detected
- `~/.codeium/windsurf/skills/` — not present or no matching skills detected
- `~/.qwen/skills/` — not present or no matching skills detected
- `~/.kiro/skills/` — not present or no matching skills detected
- `~/.openclaw/skills/` — not present or no matching skills detected

### Project skill directories

- `skills/` — not present or no matching skills detected
- `.opencode/skills/` — not present or no matching skills detected
- `.claude/skills/` — not present or no matching skills detected
- `.gemini/skills/` — not present or no matching skills detected
- `.cursor/skills/` — not present or no matching skills detected
- `.github/skills/` — not present or no matching skills detected
- `.codex/skills/` — not present or no matching skills detected
- `.qwen/skills/` — not present or no matching skills detected
- `.kiro/skills/` — not present or no matching skills detected
- `.openclaw/skills/` — not present or no matching skills detected
- `.pi/skills/` — not present or no matching skills detected
- `.agent/skills/` — not present or no matching skills detected
- `.agents/skills/` — not present or no matching skills detected
- `.atl/skills/` — not present or no matching skills detected

### Project convention files

- `AGENTS.md` — project instructions and repository conventions

## Contract

**Delegator use only.** This registry is an index, not a generated skill summary. Any agent that launches subagents should use it to select relevant skills, then pass exact `SKILL.md` paths for the subagent to read before work.

`SKILL.md` remains the source of truth. Do not inject compacted rules by default; pass paths so subagents load the full runtime contract and preserve author intent.

## Skills

| Skill | Trigger / description | Scope | Path |
| --- | --- | --- | --- |
| `branch-pr` | Create Gentle AI pull requests with issue-first checks. Trigger: creating, opening, or preparing PRs for review. | user | `/Users/aldair/.config/opencode/skills/branch-pr/SKILL.md` |
| `chained-pr` | Trigger: PRs over 400 lines, stacked PRs, review slices. Split oversized changes into chained PRs that protect review focus. | user | `/Users/aldair/.config/opencode/skills/chained-pr/SKILL.md` |
| `cognitive-doc-design` | Design docs that reduce cognitive load. Trigger: writing guides, READMEs, RFCs, onboarding, architecture, or review-facing docs. | user | `/Users/aldair/.config/opencode/skills/cognitive-doc-design/SKILL.md` |
| `comment-writer` | Write warm, direct collaboration comments. Trigger: PR feedback, issue replies, reviews, Slack messages, or GitHub comments. | user | `/Users/aldair/.config/opencode/skills/comment-writer/SKILL.md` |
| `go-testing` | Trigger: Go tests, go test coverage, Bubbletea teatest, golden files. Apply focused Go testing patterns. | user | `/Users/aldair/.config/opencode/skills/go-testing/SKILL.md` |
| `issue-creation` | Create Gentle AI issues with issue-first checks. Trigger: creating GitHub issues, bug reports, or feature requests. | user | `/Users/aldair/.config/opencode/skills/issue-creation/SKILL.md` |
| `judgment-day` | Trigger: judgment day, dual review, adversarial review, juzgar. Run blind dual review, fix confirmed issues, then re-judge. | user | `/Users/aldair/.config/opencode/skills/judgment-day/SKILL.md` |
| `skill-creator` | Trigger: new skills, agent instructions, documenting AI usage patterns. Create LLM-first skills with valid frontmatter. | user | `/Users/aldair/.config/opencode/skills/skill-creator/SKILL.md` |
| `skill-improver` | Trigger: improve skills, audit skills, refactor skills, skill quality. Audit and upgrade existing LLM-first skills. | user | `/Users/aldair/.config/opencode/skills/skill-improver/SKILL.md` |
| `work-unit-commits` | Plan commits as reviewable work units. Trigger: implementation, commit splitting, chained PRs, or keeping tests and docs with code. | user | `/Users/aldair/.config/opencode/skills/work-unit-commits/SKILL.md` |

## Excluded skills

- SDD executor/orchestrator skills (`sdd-*`) are intentionally excluded from this runtime registry.
- `_shared` is reference material, not an invokable skill.
- `skill-registry` is excluded to avoid self-referential registry refresh loops.

## Loading protocol

1. Match task context and target files against the `Trigger / description` column.
2. Pass only the matching `Path` values to the subagent under `## Skills to load before work`.
3. Instruct the subagent to read those exact `SKILL.md` files before reading, writing, reviewing, testing, or creating artifacts.
4. If no matching skill exists, proceed without project skill injection and report `skill_resolution: none`.
